import React from "react";
import { observer, inject } from "mobx-react";
import "./../css/DogBreed.css";
import ClipLoader from "react-spinners/ClipLoader";
import { css } from "@emotion/core";

const override = css`
  display: block;
  margin: 0 auto;
  border-color: white;
`;

@inject("rootStore")
@observer
class Results extends React.Component {
  render() {
    const { rootStore } = this.props;
    return (
      <React.Fragment>
        <img
          className="back"
          src={require("./../images/back.png")}
          alt="home"
          onClick={this.handleClick}
        />
        <table className="resultTable2">
          <thead>
            <tr className="resultTr">
              <th>Image</th>
              <th>Result</th>
              <th>Score</th>
              <th>Date</th>
              <th>Time</th>
            </tr>
          </thead>
          {rootStore.dogBreedStore.dogBreedResults !== undefined
            ? rootStore.dogBreedStore.dogBreedResults.map((item, index) =>
                item !== undefined && item !== "" ? (
                  <tbody key={index}>
                    <tr className="resultTr">
                      <td>
                        <img
                          className="resultImage"
                          alt="dog"
                          src={`data:image/jpeg;base64,${
                            item !== undefined ? item.file : ""
                          }`}
                        ></img>
                      </td>
                      <td>
                        {item !== undefined
                          ? item.predictionResults.name
                          : null}
                      </td>
                      <td>
                        {Math.round(
                          (item.predictionResults.score * 100 +
                            Number.EPSILON) *
                            100
                        ) / 100}
                        %
                      </td>
                      <td>{item.dateCreated.split("T")[0]}</td>
                      <td>{item.dateCreated.split("T")[1].split(".")[0]}</td>
                    </tr>
                  </tbody>
                ) : null
              )
            : "Refresh page, there is problem with database"}
        </table>
        <div className="sweet-loading">
          <ClipLoader
            css={override}
            size={40}
            color={"#123abc"}
            loading={rootStore.dogBreedStore.isLoading}
          />
        </div>
        {!rootStore.dogBreedStore.noMoreResults &&
        rootStore.dogBreedStore.dogBreedResults.length > 4 ? (
          <button onClick={this.handleLoadMoreClick}>Load More</button>
        ) : null}
      </React.Fragment>
    );
  }

  handleLoadMoreClick = () => {
    const { rootStore } = this.props;
    rootStore.dogBreedStore.data = rootStore.dogBreedStore.data + 5;
    rootStore.dogBreedStore.getResults(false, rootStore.dogBreedStore.data);
  };

  handleClick = () => {
    const { rootStore } = this.props;
    rootStore.routerStore.goTo("dogbreed");
  };
}

export default Results;
