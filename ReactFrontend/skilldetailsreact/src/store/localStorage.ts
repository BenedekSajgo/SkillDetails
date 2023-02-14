export const loadState = () => {
    try {
      const result = localStorage.getItem("web3_state");
      if (result === null) {
        return undefined;
      }
  
      return JSON.parse(result);
    } catch (error) {
      return undefined;
    }
  };
  
  export const saveState = (state: any) => {
    try {
      const serializedState = JSON.stringify(state);
      localStorage.setItem("web3_state", serializedState);
    } catch (error) {
      console.log(error);
    }
  };
  