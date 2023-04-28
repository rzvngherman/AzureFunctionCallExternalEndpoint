1) project AZCECoreWebApi: 
	- aspnet core wep api
	- has an endpoint GET 'https://localhost:7150/WeatherForecast'
	- call sql server to get data

2) project AZCEAzureFunction 
	- azure http function
	- function 'CallExternalWeatherForecastFunction' with endpoint GET http://localhost:8085/api/CallExternalWeatherForecastFunction
	- makes a call to (1) AZCECoreWebApi, through endpoint GET '/WeatherForecast'
	- added cores for port 3000, on localhost (for FE url)
	
3)(*deleted)
	project BlazorServerApp1 
	- Blazor Server 
	- makes a call to (2) AZCEAzureFunction, through endpoint GET '/api/CallExternalWeatherForecastFunction'

3) project ReactServerApp1 
	- react FE Server 
	- makes a call to (2) AZCEAzureFunction, through endpoint GET '/api/CallExternalWeatherForecastFunction'
