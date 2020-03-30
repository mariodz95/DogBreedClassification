import React from "react";
import { observer, inject } from "mobx-react";
import "./../css/form.css";

@inject("rootStore")
@observer
class Login extends React.Component {
  render() {
    const { rootStore } = this.props;
    return (
      <React.Fragment>
        <form className="form">
          <label>{rootStore.dogBreedStore.form.$("email").label}</label>
          <input {...rootStore.dogBreedStore.form.$("email").bind()} />
          <p className="formError">
            {rootStore.dogBreedStore.form.$("email").error}
          </p>
          <label htmlFor={rootStore.dogBreedStore.form.$("password")}>
            {rootStore.dogBreedStore.form.$("password").label}
          </label>
          <input
            {...rootStore.dogBreedStore.form.$("password").bind()}
            type={rootStore.dogBreedStore.passwordType}
          />
          <span
            className="password__show"
            onClick={rootStore.dogBreedStore.changePasswordType}
          >
            {rootStore.dogBreedStore.passwordType === "text"
              ? "Hide Password"
              : "Show Password"}
          </span>
          <p className="formError">
            {rootStore.dogBreedStore.form.$("password").error}
          </p>
          {rootStore.dogBreedStore.loginFailed ? (
            <p className="formError">Email or password is incorrect</p>
          ) : null}
          <button
            className="formButton"
            type="button"
            onClick={rootStore.dogBreedStore.form.onClear}
          >
            Clear
          </button>
          <button
            className="formButton"
            type="submit"
            onClick={rootStore.dogBreedStore.form.onSubmit}
          >
            Submit
          </button>
          <p className="formError">{rootStore.dogBreedStore.form.error}</p>
          <br />
          <a onClick={this.handleClick}>Back to registration</a>
        </form>
      </React.Fragment>
    );
  }

  handleClick = () => {
    const { rootStore } = this.props;
    rootStore.routerStore.goTo("registration");
  };
}

export default Login;
