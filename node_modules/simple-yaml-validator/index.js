var yaml = require('js-yaml')
var parseYaml = yaml.safeLoad

var yamlTypeMap = {
	string: function(input) {
		return typeof input === 'string'
	},
	number: function(input) {
		return typeof input === 'number'
	},
	binary: function(input) {
		return Buffer.isBuffer(input)
	},
	timestamp: function(input) {
		return input && input instanceof Date
	}
}

function yamlResourceIsValidForYamlSchema(yamlResource, yamlSchema) {
	return jsonResourceIsValidForJsonSchema(parseYaml(yamlResource), parseYaml(yamlSchema))
}

function elementIsValid(schema) {
	return function(element) {
		return jsonResourceIsValidForJsonSchema(element, schema)
	}
}

function jsonResourceIsValidForJsonSchema(jsonResource, jsonSchema) {
	if (Array.isArray(jsonSchema)) {
		return jsonResource && Array.isArray(jsonResource) && jsonResource.every(elementIsValid(jsonSchema[0]))
	} else {
		return Object.keys(jsonSchema).every(function (key) {
			if (Array.isArray(jsonSchema[key])) {
				return jsonResource[key] && Array.isArray(jsonResource[key]) && jsonResource[key].every(elementIsValid(jsonSchema[key][0]))
			} else if (typeof jsonSchema[key] === 'object') {
				return jsonResourceIsValidForJsonSchema(jsonResource[key], jsonSchema[key])
			} else {
				return yamlTypeMap[jsonSchema[key]] && yamlTypeMap[jsonSchema[key]](jsonResource[key])
			}
		})
	}
}

module.exports = function resourceValidator(yamlSchema) {
	return function validate(yamlResource) {
		return yamlResourceIsValidForYamlSchema(yamlResource, yamlSchema)
	}
}
