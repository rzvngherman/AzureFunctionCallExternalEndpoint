import logo from './logo.svg';
import './App.css';
import React, { Component } from 'react';
// import { Collapse, Button } from "reactstrap"

import { WeatherForecast } from './pages/WeatherForecast/WeatherForecast';
import { BrowserRouter, Route, Routes, NavLink } from 'react-router-dom';

function App() {
    return (
        <BrowserRouter>
            <div className="App container">
                <h3 className="d-flex justify-content-center m-3">
                    My React App
                </h3>

                <nav className="navbar navbar-expand-sm bg-light navbar-dark">
                    <ul className="navbar-nav">
                        <li className="nav-item m-1">
                            <NavLink className="btn btn-light btn-outline-primary" to="/home">
                                Home
                            </NavLink>
                        </li>
                        <li className="nav-item m-1">
                            <NavLink className="btn btn-light btn-outline-primary" to="/weatherforecast">
                                Call Azure Function
                            </NavLink>
                        </li>
                    </ul>
                </nav>

                <Routes>
                    <Route path="/weatherforecast" element={<WeatherForecast />}></Route>
                </Routes>
            </div>
        </BrowserRouter>
    );
}
export default App;