import React from "react";
import ImageUploader from "react-images-upload";
import "./../css/DogBreed.css";
import ClipLoader from "react-spinners/ClipLoader";
import { css } from "@emotion/core";
import { inject, observer } from "mobx-react";
import Popup from "reactjs-popup";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const override = css`
  display: block;
  margin: 0 auto;
  border-color: white;
`;

@inject("rootStore")
@observer
class DogBreed extends React.Component {
  componentDidMount() {
    this.scrollToBottom();
  }

  componentDidUpdate() {
    this.scrollToBottom();
  }
  notify = () => toast.success("Welcome!");
  render() {
    const { rootStore } = this.props;
    if (rootStore.dogBreedStore.toast) {
      this.notify();
      rootStore.dogBreedStore.displayToast();
    }
    return (
      <React.Fragment>
        <ToastContainer />
        <div className="sidenav">
          <img
            src={require("./../images/history.png")}
            alt="results"
            onClick={this.handleClick}
          />
          <img
            src={require("./../images/info.png")}
            alt="info"
            onClick={rootStore.dogBreedStore.handleOpenModal}
          />
          <img
            src={require("./../images/logout.png")}
            alt="logout"
            onClick={this.handleLogoutClick}
          />
        </div>
        <div className="App">
          <Popup
            open={rootStore.dogBreedStore.showModal}
            closeOnDocumentClick
            onClose={rootStore.dogBreedStore.handleCloseModal}
          >
            <div className="modal">
              <a
                className="close"
                onClick={rootStore.dogBreedStore.handleCloseModal}
              >
                &times;
              </a>
              <img src={require("./../images/ferit.png")} alt="ferit" />
              This web app is made for RUAP and can predict 120 dog breeds.
            </div>
          </Popup>
          <ImageUploader
            className="ImgUpload"
            withIcon={true}
            buttonText="Choose image"
            onChange={rootStore.dogBreedStore.onDrop}
            imgExtension={[".jpg", ".png"]}
            maxFileSize={5242880}
            withPreview={true}
            singleImage={true}
            fileContainerStyle={{ backgroundColor: "#282c34" }}
          />
          <div className="sweet-loading">
            <ClipLoader
              css={override}
              size={40}
              color={"#123abc"}
              loading={rootStore.dogBreedStore.isLoading}
            />
          </div>
          <button
            className="BtnUpload"
            variant="success"
            onClick={this.uploadHandler}
            disabled={rootStore.dogBreedStore.uploadedImage === null}
            size="sm"
          >
            Classify dog breed
          </button>
          {(rootStore.dogBreedStore.prediction.data !== null) &
          (rootStore.dogBreedStore.prediction.data !== undefined) ? (
            <React.Fragment>
              <h2>Top 5 Scores</h2>
              {rootStore.dogBreedStore.prediction.data.map((item, index) => (
                <table className="resultTable" key={index}>
                  <thead>
                    <tr>
                      <th>Dog breed</th>
                      <th>Score</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="resultTr">
                      <td>{item.name}</td>
                      <td>
                        {Math.round((item.score * 100 + Number.EPSILON) * 100) /
                          100}
                        %
                      </td>
                    </tr>
                  </tbody>
                </table>
              ))}
            </React.Fragment>
          ) : null}
        </div>
        <div
          style={{ float: "left", clear: "both" }}
          ref={(el) => {
            this.messagesEnd = el;
          }}
        ></div>
      </React.Fragment>
    );
  }

  scrollToBottom = () => {
    this.messagesEnd.scrollIntoView({ behavior: "smooth" });
  };

  handleClick = () => {
    const { rootStore } = this.props;
    rootStore.dogBreedStore.imageRemove();
    rootStore.dogBreedStore.removeResult();
    rootStore.routerStore.goTo("results");
  };

  handleLogoutClick = () => {
    const { rootStore } = this.props;
    rootStore.dogBreedStore.imageRemove();
    rootStore.dogBreedStore.removeResult();
    rootStore.dogBreedStore.userRemove();
    rootStore.routerStore.goTo("login");
  };

  uploadHandler = () => {
    const { rootStore } = this.props;
    const formData = new FormData();
    formData.append(
      "formData",
      rootStore.dogBreedStore.uploadedImage,
      rootStore.dogBreedStore.uploadedImage.name
    );
    rootStore.dogBreedStore.getPrediction(
      formData,
      rootStore.dogBreedStore.user.data.id
    );
  };
}

export default DogBreed;
