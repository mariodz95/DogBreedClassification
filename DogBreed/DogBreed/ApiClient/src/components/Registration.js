import React from "react";
import { observer, inject } from "mobx-react";
import RegistrationForm from "./RegistrationForm";

const form = new RegistrationForm();

@inject("rootStore")
@observer
class Registration extends React.Component {
  render() {
    return (
      <React.Fragment>
        <form>
          <label>{form.$("email").label}</label>
          <input {...form.$("email").bind()} />
          <p>{form.$("email").error}</p>

          <label htmlFor={form.$("password")}>{form.$("password").label}</label>
          <input {...form.$("password").bind()} />
          <p>{form.$("password").error}</p>

          <label htmlFor={form.$("passwordConfirm")}>
            {form.$("passwordConfirm").label}
          </label>
          <input {...form.$("passwordConfirm").bind()} />
          <p>{form.$("passwordConfirm").error}</p>

          <button type="submit" onClick={form.onSubmit}>
            Submit
          </button>
          <button type="button" onClick={form.onClear}>
            Clear
          </button>
          <button type="button" onClick={form.onReset}>
            Reset
          </button>

          <a onClick={this.handleClick}>Alread have account</a>

          <p>{form.error}</p>
        </form>
      </React.Fragment>
    );
  }

  handleClick = () => {
    const { rootStore } = this.props;
    rootStore.routerStore.goTo("login");
  };
}

export default Registration;
