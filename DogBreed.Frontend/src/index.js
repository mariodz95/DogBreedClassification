import React from "react";
import ReactDOM from "react-dom";
import "./css/index.css";
import * as serviceWorker from "./serviceWorker";
import App from "./App";
import { configure } from "mobx";

//ReactDOM.render(<DogBreed />, document.getElementById("root"));

// Enable strict mode for MobX. This disallows state changes outside of an action
configure({ enforceActions: "observed" });

ReactDOM.render(<App />, document.getElementById("root"));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
