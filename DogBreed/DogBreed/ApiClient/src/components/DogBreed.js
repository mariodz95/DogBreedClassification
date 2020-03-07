import React from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import ImageUploader from "react-images-upload";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../DogBreed.css";
import ClipLoader from "react-spinners/ClipLoader";
import { css } from "@emotion/core";

const override = css`
  display: block;
  margin: 0 auto;
  border-color: red;
`;

let dogBreedData = null;
const data = "background-color: #282c34";
class DogBreed extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      picture: null,
      loading: false
    };
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
          <Button
            className="BtnUpload"
            variant="success"
            onClick={this.uploadHandler}
            disabled={!this.state.picture}
            size="sm"
          >
            Upload Image!
          </Button>
          <div className="sweet-loading">
            <ClipLoader
              css={override}
              size={150}
              //size={"150px"} this also works
              color={"#123abc"}
              loading={this.state.loading}
            />
          </div>
        </div>
      </React.Fragment>
    );
  }

  onDrop = event => {
    this.setState({
      picture: event[0]
    });
  };

  isLoading = set => {
    console.log("Event:", set);
    this.setState({
      loading: set
    });
  };
  notify = () => toast("You nedd to upload image!");

  uploadHandler = () => {
    this.notify();
    const formData = new FormData();
    formData.append("formData", this.state.picture, this.state.picture.name);
    this.isLoading(true);
    axios
      .post("https://localhost:5001/api/predict/classify", formData)
      .then(res => {
        console.log("resp", res);
        dogBreedData = res;
        this.isLoading(false);
      });
  };
}

export default DogBreed;
