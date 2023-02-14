import React from "react";
import PersonDetail from "../Components/PersonDetail";
import PersonSearchBar from "../Components/PersonSearchBar";
import { Divider } from "antd";

const PersonScreen = () => {
  return (
    <div className="root">
      <PersonSearchBar></PersonSearchBar>
      <PersonDetail></PersonDetail>
    </div>
  );
};

export default PersonScreen;
