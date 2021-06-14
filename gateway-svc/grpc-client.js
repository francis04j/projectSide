const grpc = require('@grpc/grpc-js')
const protoLoader = require("@grpc/proto-loader");
const protoFiles = require('google-proto-files');
const PROTO_PATH = '../protos/rent.proto';


const packageDefinition = protoLoader.loadSync(PROTO_PATH, 
{
    keepCase: true,
    longs: String,
    enums: String,
    defaults: true,
    oneofs: true,
    includeDirs: [__dirname + protoFiles, '.']
    },
);
const rentPkg = grpc.loadPackageDefinition(packageDefinition).rentPkg;


const rentClient = new rentPkg.rent("localhost:40000", grpc.credentials.createInsecure());

module.exports = rentClient;