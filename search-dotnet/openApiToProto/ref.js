function $RefParser () {
    
    this.schema = null;
  
}

$RefParser.parse = function parse (path, schema, options, callback) {
    let Class = this; // eslint-disable-line consistent-this
    let instance = new Class();
    return instance.parse.apply(instance, arguments);
  };

  $RefParser.prototype.parse = async function parse (path, schema, options, callback) {
      console.log('break', arguments);
    console.log('Path', path)
    }

module.exports = $RefParser;
module.exports.default = $RefParser;