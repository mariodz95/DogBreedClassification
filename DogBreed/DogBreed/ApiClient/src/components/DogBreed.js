import React from "react";
import axios from "axios";
import ImageUploader from "react-images-upload";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./../DogBreed.css";
import ClipLoader from "react-spinners/ClipLoader";
import { css } from "@emotion/core";

const override = css`
  display: block;
  margin: 0 auto;
  border-color: white;
`;

let dogBreedData = null;

class DogBreed extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      picture: null,
      loading: false
    };
  }

  componentDidMount() {
    this.scrollToBottom();
  }

  componentDidUpdate() {
    console.log("Test");
    this.scrollToBottom();
    dogBreedData = null;
  }

  render() {
    return (
      <React.Fragment>
        <div className="App">
          <ToastContainer />
          <ImageUploader
            className="ImgUpload"
            withIcon={true}
            buttonText="Choose image"
            onChange={this.onDrop}
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
              loading={this.state.loading}
            />
          </div>

          <button
            className="BtnUpload"
            variant="success"
            onClick={this.uploadHandler}
            disabled={!this.state.picture}
            size="sm"
          >
            Classify dog breed
          </button>
          {(dogBreedData !== null) & (dogBreedData !== undefined)
            ? dogBreedData.data.map((item, index) => (
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
              ))
            : null}
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

  onDrop = event => {
    this.setState({
      picture: event[0]
    });
  };

  isLoading = set => {
    this.setState({
      loading: set
    });
  };

  scrollToBottom = () => {
    this.messagesEnd.scrollIntoView({ behavior: "smooth" });
  };

  uploadHandler = () => {
    const formData = new FormData();
    formData.append("formData", this.state.picture, this.state.picture.name);
    this.isLoading(true);
    axios
      .post("https://localhost:44368/api/predict/classify", formData)
      .then(res => {
        dogBreedData = res;
        console.log("resp", dogBreedData);
        this.isLoading(false);
      });
  };
}

export default DogBreed;
