import React from "react";
import { useSelector } from "react-redux";
import { Route, Routes } from "react-router-dom";
import NavBar from "../Components/NavBar";
import HomeScreen from "../screens/HomeScreen";
import NotFoundScreen from "../screens/NotFoundScreen";
import PersonScreen from "../screens/PersonScreen";
import "./RootNavigator.css";

const RootNavigator = () => {
  const collapsedState = useSelector((state: any) => state.collapsed);

  return (
    <div className="root">
      <NavBar></NavBar>
      <div style={{ marginLeft: collapsedState ? 60 : 200 }}>
        <Routes>
          <Route path="/" element={<HomeScreen />} />
          <Route path="*" element={<NotFoundScreen />} />
          <Route path="/consultants">
            <Route index element={<PersonScreen />} />
            <Route path=":id" element={<PersonScreen />} />
          </Route>
          <Route path="/skills" element={<div>skills</div>} />
          <Route path="/login" element={<div>login</div>} />
        </Routes>
      </div>
    </div>
  );
};

export default RootNavigator;
