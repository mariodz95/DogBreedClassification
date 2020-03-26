// Routes are matched from top to bottom. Make sure they are sequenced
// in the order of priority. It is generally best to sort them by pattern,
// prioritizing specific patterns over generic patterns (patterns with
// one or more parameters). For example:
//     /items
//     /items/:id
export const routes = [
  {
    name: "dogbreed",
    pattern: "/"
  },
  {
    name: "login",
    pattern: "/login"
  },
  {
    name: "registration",
    pattern: "/registration"
  },
  {
    name: "results",
    pattern: "/results",
    onEnter: (fromState, toState, routerStore) => {
      const {
        rootStore: { dogBreedStore }
      } = routerStore;
      dogBreedStore.changeLoading(true);
      dogBreedStore.getResults(true, 0);
      dogBreedStore.changeLoading(false);
    }
  }
];
