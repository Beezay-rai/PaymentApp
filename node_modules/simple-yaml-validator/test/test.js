var test = require('tape')
var fs = require('fs')
var Validator = require('../')

test('very simple', function(t) {
	var validate = Validator(fs.readFileSync('test/schemas/very-simple.yaml', 'utf8'))
	t.ok(validate(fs.readFileSync('test/resources/very-simple__good.yaml', 'utf8')), 'schema should validate')
	t.notOk(validate(fs.readFileSync('test/resources/very-simple__bad.yaml', 'utf8')), 'schema should not validate')
	t.end()
})

test('more complicated', function(t) {
	var validate = Validator(fs.readFileSync('test/schemas/more-complicated.yaml', 'utf8'))
	t.ok(validate(fs.readFileSync('test/resources/more-complicated__good.yaml', 'utf8')), 'schema should validate')
	t.notOk(validate(fs.readFileSync('test/resources/more-complicated__bad1.yaml', 'utf8')), 'schema should not validate')
	t.notOk(validate(fs.readFileSync('test/resources/more-complicated__bad2.yaml', 'utf8')), 'schema should not validate')
	t.end()
})

test('timestamp', function(t) {
	var validate = Validator(fs.readFileSync('test/schemas/timestamp.yaml', 'utf8'))
	t.ok(validate(fs.readFileSync('test/resources/timestamp__good1.yaml', 'utf8')), 'schema should validate')
	t.ok(validate(fs.readFileSync('test/resources/timestamp__good2.yaml', 'utf8')), 'schema should validate')
	t.ok(validate(fs.readFileSync('test/resources/timestamp__good3.yaml', 'utf8')), 'schema should validate')
	t.notOk(validate(fs.readFileSync('test/resources/timestamp__bad1.yaml', 'utf8')), 'schema should not validate')
	t.notOk(validate(fs.readFileSync('test/resources/timestamp__bad2.yaml', 'utf8')), 'schema should not validate')
	t.end()
})

test('deeper', function(t) {
	var validate = Validator(fs.readFileSync('test/schemas/deeper.yaml', 'utf8'))
	t.ok(validate(fs.readFileSync('test/resources/deeper__good.yaml', 'utf8')), 'schema should validate')
	t.notOk(validate(fs.readFileSync('test/resources/deeper__bad1.yaml', 'utf8')), 'schema should not validate')
	t.notOk(validate(fs.readFileSync('test/resources/deeper__bad2.yaml', 'utf8')), 'schema should not validate')
	t.end()
})

test('arrays', function(t) {
	var validate = Validator(fs.readFileSync('test/schemas/arrays.yaml', 'utf8'))
	t.ok(validate(fs.readFileSync('test/resources/arrays__good1.yaml', 'utf8')), 'schema should validate')
	t.ok(validate(fs.readFileSync('test/resources/arrays__good2.yaml', 'utf8')), 'schema should validate')
	t.notOk(validate(fs.readFileSync('test/resources/arrays__bad1.yaml', 'utf8')), 'schema should not validate')
	t.notOk(validate(fs.readFileSync('test/resources/arrays__bad2.yaml', 'utf8')), 'schema should not validate')
	t.end()
})

test('array only', function(t) {
	var validate = Validator(fs.readFileSync('test/schemas/array-only.yaml', 'utf8'))
	t.ok(validate(fs.readFileSync('test/resources/array-only__good.yaml', 'utf8')), 'schema should validate')
	t.notOk(validate(fs.readFileSync('test/resources/array-only__bad1.yaml', 'utf8')), 'schema should not validate')
	t.end()
})
