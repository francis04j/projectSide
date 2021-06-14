const express = require('express');
const rentClient = require('./grpc-client');

const app = express();
app.use(express.json());
app.use(express.urlencoded({
    extended: true
}));

app.post('/rent', (req,res) => {
  
    rentClient.rentCar(req.body, (err, response) => {
             if(err) throw err;
 
             console.log('createQuestion: Received from server: ' + JSON.stringify(response));
             res.status(201).send(JSON.stringify({"message": `Receipt ID is ${response.receiptId}`}));
         });
     
    
 });

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
	console.log("Server running at port %d", PORT);
});