import React from "react";
import ImageUploader from "react-images-upload";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./../css/DogBreed.css";
import ClipLoader from "react-spinners/ClipLoader";
import { css } from "@emotion/core";
import { inject, observer } from "mobx-react";

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
  render() {
    const { rootStore } = this.props;
    return (
      <React.Fragment>
        <div className="sidenav">
          <img
            src={require("./../images/login.png")}
            alt="home"
            onClick={this.handleClick}
          />
          <img
            src={require("./../images/home.png")}
            alt="home"
            onClick={this.handleClick}
          />
          <img
            src={require("./../images/info.png")}
            alt="home"
            onClick={this.handleClick}
          />
        </div>
        <div className="App">
          <ToastContainer />
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
            disabled={!rootStore.dogBreedStore.uploadedImage}
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
                    <tr>
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
          ref={el => {
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
    rootStore.routerStore.goTo("results");
  };

  uploadHandler = () => {
    const { rootStore } = this.props;
    const formData = new FormData();
    console.log("t", rootStore.dogBreedStore.uploadedImage);
    formData.append(
      "formData",
      rootStore.dogBreedStore.uploadedImage,
      rootStore.dogBreedStore.uploadedImage.name
    );
    rootStore.dogBreedStore.getPrediction(formData);
  };
}

export default DogBreed;
