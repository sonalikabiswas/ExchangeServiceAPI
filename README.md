# ExchangeServiceAPI

This is an Exchange Service API which uses https://v6.exchangerate-api.com/v6 to exchange the value from one currency to another

Sample curl command:

curl -X 'POST' \
  'https://localhost:7078/ExchangeService' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "amount": 100,
  "inputCurrency": "AUD",
  "outputCurrency": "USD"
}'

Request URL :https://localhost:7078/ExchangeService or https://localhost:7078/ExchangeService?api-version=1 

Response:
{
  "amount": 100,
  "inputCurrency": "AUD",
  "outputCurrency": "USD",
  "value": 65.09
}


Features of the API:
Developed in .NET 6 
Uses dependency injection in the controller for handler and logging 
Has required field validations
Exception handling for scenarios when remote API fails to convert
Console Logging using Serilog
Versioning
Unit tests to cover Ok and 500 status response

Features which are not covered but would have added in additional time
More Unit test coverage
Validations for incorrect currency types
Logging in DB or flat file 
Upgraded to .NET 8 
More error handling 
Configurations to be read from Database


