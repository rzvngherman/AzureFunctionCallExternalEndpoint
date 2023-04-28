import React, { Component } from 'react';

export class WeatherForecast extends Component {

    /*
    These methods are called in the following order when an instance of a component 
    is being created and inserted into the DOM:
        constructor()
        static getDerivedStateFromProps()
        render()
        componentDidMount()
    */

    //1
    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true};
    }

    //2
    //static getDerivedStateFromProps(props, state) {
    //}

    //3
    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : this.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Calling external endpoint from azure function</h1>
                {contents}
            </div>
        );
    }

    //4
    componentDidMount() {
        this.populateWeatherData();
    }

    renderForecastsTable(forecastData) {

        return (
            <div className="container2">

                <p>function uri '{forecastData.FunctionUri}', base-url '{forecastData.BaseUrl}',  method '{forecastData.FunctionMethod}'</p>
                <p>external endpoint called from function '{forecastData.uri3RdPartyCalled}', base-url '{forecastData.base3RdPartyCalled}', methodType '{forecastData.uri3RdPartyMethod.method}'.</p>
                <p>Data retrieved:</p>

                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Temp. (C)</th>
                            <th>Temp. (F)</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        {forecastData.uri3RdPartyResults.map(forecast =>
                            <tr key={forecast.date}>
                                <td>{forecast.date}</td>
                                <td>{forecast.temperatureC}</td>
                                <td>{forecast.temperatureF}</td>
                                <td>{forecast.summary}</td>
                            </tr>
                        )}
                    </tbody>
                </table>

            </div>
        );
    }

    async populateWeatherData() {

        var url = 'http://localhost:8085/api/CallExternalWeatherForecastFunction';

        try {
            const response = await fetch(url);
            const data = await response.json();
            this.setState({ forecasts: data, loading: false });
        }
        catch (e) {
            console.log(e);
        }
    }
}