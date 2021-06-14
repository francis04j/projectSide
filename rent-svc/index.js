
const queries = []

function rentCar(ctx) {
    console.log('recieved request', ctx.req)
    const qs = {
        'id': queries.length + 1,
        'text': ctx.req.carId
    }
    queries.push(qs);
    ctx.res = {'receiptId': queries.length }
}

function getRentQueries(ctx) { 
    ctx.res = {"questions": queries}; 
}

exports.rentCar = rentCar
exports.getRentQueries = getRentQueries;
