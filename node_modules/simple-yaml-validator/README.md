# simple-yaml-validator

Validate YAML strings against a simple YAML schema.

## install

The normal npm way:

```sh
npm install simple-yaml-validator
```

## use

What you'll need is a YAML schema file that defines what required fields are (check out one
of these [example schemas](./test/schemas/)), and the YAML data to validate. Pass them in as strings:

```js
var Validator = require('simple-yaml-validator')
var validate = Validator(schemaString)
var isValid = validate(resourceString)
```

## data types

Supported data types are:

* string
* number
* binary
* timestamp

## YAML schema

Every field specified in the schema is required to exist, and the property must exist.
There's plenty of examples in the [test folder](./test/schemas/). Basically
just write the YAML file that you want, like so:

```yaml
---
street_name: string
house_number: number
...
```

For deeper objects, you just write the required fields of the object:

```yaml
---
address:
  street: string
  apartment: number
...
```

For arrays, write the object that's required in the array:

```yaml
---
people:
  - first_name: string
    last_name: string
    age: number
...
```

## license

[VOL](http://veryopenlicense.com)
