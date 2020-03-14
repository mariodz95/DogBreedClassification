import React from "react";
import { inject } from "mobx-react";
import { RouterView } from "mobx-state-router";
import DogBreed from "./components/DogBreed";
import Results from "./components/Results";

const viewMap = {
  dogbreed: <DogBreed />,
  results: <Results />
};

export const Shell = inject("rootStore")(
  class extends React.Component {
    render() {
      const { rootStore } = this.props;
      const { routerStore } = rootStore;

      return <RouterView routerStore={routerStore} viewMap={viewMap} />;
    }
  }
);
