import { Button } from "antd";
import { useState } from "react";
import { PoweroffOutlined } from "@ant-design/icons";
import "./css/HomeScreen.css";

const HomeScreen = () => {
  const [loading, setLoading] = useState(false);

  const handleClick = () => {
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
    }, 1000);
  };

  return (
    <Button
      type="primary"
      icon={<PoweroffOutlined />}
      loading={loading}
      className="my-button"
      onClick={handleClick}
    >
      Go to consultants
    </Button>
  );
};
export default HomeScreen;
