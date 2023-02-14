import { Menu, MenuProps } from "antd";
import {
  HomeOutlined,
  UserOutlined,
  ToolOutlined,
  PoweroffOutlined,
  LeftOutlined,
  RightOutlined,
} from "@ant-design/icons/lib/icons";
import "./css/NavBar.css";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { isCollapsed } from "../store/collapsed/slice";

type MenuItem = Required<MenuProps>["items"][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[]
): MenuItem {
  return { key, icon, children, label } as MenuItem;
}

const NavBar = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const collapsedState = useSelector((state: any) => state.collapsed);

  const items: MenuItem[] = [
    getItem(
      "",
      "collapse",
      collapsedState ? <RightOutlined /> : <LeftOutlined />
    ),
    getItem("Home", "/", <HomeOutlined />),
    getItem("Consultants", "/consultants", <UserOutlined />),
    getItem("Skills", "/skills", <ToolOutlined />),
  ];
  const logout: MenuItem[] = [
    getItem("Logout", "signout", <PoweroffOutlined />),
  ];

  let href = window.location.href.split("/");
  let route = href[3];

  return (
    <div className="navbar">
      <Menu
        className="menu"
        onClick={({ key }) => {
          if (key === "collapse") {
            dispatch(isCollapsed());
          } else {
            navigate(key);
          }
        }}
        defaultSelectedKeys={[`/${route}`]}
        selectedKeys={[`/${route}`]}
        mode="inline"
        inlineCollapsed={collapsedState}
        style={{ width: collapsedState ? 60 : 200, borderColor: "#d9d9d9" }}
        items={items}
      />
      <Menu
        className="logoutmenu"
        onClick={({ key }) => {
          if (key === "signout") {
            console.log("signout");
          }
        }}
        mode="inline"
        inlineCollapsed={collapsedState}
        style={{ width: collapsedState ? 60 : 200, borderColor: "#d9d9d9" }}
        items={logout}
      />
    </div>
  );
};

export default NavBar;
