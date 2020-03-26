import React from "react";
import LoginForm from "./LoginForm";
import { observer, inject } from "mobx-react";
import "./../css/form.css";

@inject("rootStore")
@observer
class Login extends React.Component {
  render() {
    const { rootStore } = this.props;
    return (
      <React.Fragment>
        <form className="loginForm">
          <label>{rootStore.dogBreedStore.form.$("email").label}</label>
          <input {...rootStore.dogBreedStore.form.$("email").bind()} />
          <p>{rootStore.dogBreedStore.form.$("email").error}</p>
          <label htmlFor={rootStore.dogBreedStore.form.$("password")}>
            {rootStore.dogBreedStore.form.$("password").label}
          </label>
          <input {...rootStore.dogBreedStore.form.$("password").bind()} />
          <p>{rootStore.dogBreedStore.form.$("password").error}</p>
          <button type="submit" onClick={rootStore.dogBreedStore.form.onSubmit}>
            Submit
          </button>
          <button type="button" onClick={rootStore.dogBreedStore.form.onClear}>
            Clear
          </button>
          <p>{rootStore.dogBreedStore.form.error}</p>
        </form>
      </React.Fragment>
    );
  }
  handleClick = () => {
    const { rootStore } = this.props;
    const result = rootStore.dogBreedStore.login(
      "mariodz95@gmail.com",
      "testasdas123"
    );
    console.log("handle clikc", result);

    console.log("handle clikc promise", result.promise);
    if (result.promise) {
      console.log("handle clikc result", result);

      rootStore.routerStore.goTo("dogbreed");
    }
  };
}

export default Login;
