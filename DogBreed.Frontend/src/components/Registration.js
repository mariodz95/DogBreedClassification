import React from "react";
import { observer, inject } from "mobx-react";
import "./../css/form.css";

@inject("rootStore")
@observer
class Registration extends React.Component {
  render() {
    const { rootStore } = this.props;
    return (
      <React.Fragment>
        <div className="form">
          <form>
            <label>{rootStore.dogBreedStore.regForm.$("email").label}</label>
            <input {...rootStore.dogBreedStore.regForm.$("email").bind()} />
            <p className="formError">
              {rootStore.dogBreedStore.regForm.$("email").error}
            </p>
            <label htmlFor={rootStore.dogBreedStore.regForm.$("password")}>
              {rootStore.dogBreedStore.regForm.$("password").label}
            </label>
            <input
              {...rootStore.dogBreedStore.regForm.$("password").bind()}
              type={rootStore.dogBreedStore.passwordType}
            />
            <p className="formError">
              {rootStore.dogBreedStore.regForm.$("password").error}
            </p>
            <label
              htmlFor={rootStore.dogBreedStore.regForm.$("passwordConfirm")}
            >
              {rootStore.dogBreedStore.regForm.$("passwordConfirm").label}
            </label>
            <input
              {...rootStore.dogBreedStore.regForm.$("passwordConfirm").bind()}
              type={rootStore.dogBreedStore.passwordType}
            />
            <p className="formError">
              {rootStore.dogBreedStore.regForm.$("passwordConfirm").error}
            </p>
            <span
              className="password__show"
              onClick={rootStore.dogBreedStore.changePasswordType}
            >
              {rootStore.dogBreedStore.passwordType === "text"
                ? "Hide"
                : "Show"}
            </span>
            {rootStore.dogBreedStore.registrationFailed ? (
              <p className="formError">User with this email already exist!</p>
            ) : null}
            <button
              className="formButton"
              type="button"
              onClick={rootStore.dogBreedStore.regForm.onClear}
            >
              Clear
            </button>
            <button
              className="formButton"
              type="submit"
              onClick={rootStore.dogBreedStore.regForm.onSubmit}
            >
              Submit
            </button>
            <a onClick={this.handleClick}>Alread have account?</a>
            <p>{rootStore.dogBreedStore.regForm.error}</p>
          </form>
        </div>
      </React.Fragment>
    );
  }

  handleClick = () => {
    const { rootStore } = this.props;
    rootStore.routerStore.goTo("login");
  };
}

export default Registration;
