const mali = require('mali');
const path = require('path');
const { rentCar } = require('./index');

const PROTO_PATH = path.resolve(__dirname, '../protos/rent.proto');


const app = new mali(PROTO_PATH, 'rent'); //name has to match service name
app.use({ rentCar });
app.start("0.0.0.0:40000");
console.log(`rent service running @ ${40000}`)