import { Button, Checkbox, Input, Spin } from "antd";
import "./css/PersonSearchBar.css";
import {
  DeleteOutlined,
  ToolOutlined,
  Loading3QuartersOutlined,
} from "@ant-design/icons/lib/icons";
import { useEffect, useState } from "react";
import { PersonSearchInterface } from "../Interfaces/PersonSearchInterface";
import { PersonController } from "../Axios/PersonController";
import { Service } from "../Axios/Service";
import { Link } from "react-router-dom";

const PersonSearchBar = () => {
  const [persons, setPersons] = useState<Service<PersonSearchInterface[]>>({
    status: "loading",
  });
  const [refresh, setRefresh] = useState<boolean>(false);
  const [showDeleted, setShowDeleted] = useState<boolean>(true);
  const [search, setSearch] = useState<string>("");

  useEffect(() => {
    showDeleted
      ? PersonController.getPersonsIncludeDeleted().then((data) => {
          setPersons(data);
          console.log(data);
        })
      : PersonController.getPersons().then((data) => {
          setPersons(data);
          console.log(data);
        });
  }, [refresh, showDeleted]);

  const handleDelete = (id: number) => {
    PersonController.deletePerson(id).then(() => {
      setRefresh(!refresh);
    });
  };
  const handleRestore = (id: number) => {
    PersonController.restorePerson(id).then(() => {
      setRefresh(!refresh);
    });
  };

  return (
    <div className="bar">
      <div className="search-field">
        <Input
          style={{ borderColor: "black" }}
          placeholder="Search"
          onChange={(e) => setSearch(e.target.value)}
          onPressEnter={() => setRefresh(!refresh)}
        />
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            paddingTop: 5,
            paddingBottom: 10,
          }}
        >
          {/* <Button
            className="button"
            icon={<LeftOutlined />}
            onClick={() => console.log("collapse")}
          ></Button> */}
          <Checkbox
            checked={showDeleted}
            onChange={() => setShowDeleted(!showDeleted)}
          >
            Show deleted
          </Checkbox>
        </div>
      </div>
      <div>
        <div>
          {persons.status === "loaded" ? (
            persons.payload
              .filter((person: PersonSearchInterface) =>
                `${person.lastName} ${person.firstName}`
                  .toLowerCase()
                  .includes(search.toLowerCase())
              )
              .map((person: PersonSearchInterface) => (
                <div
                  style={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-between",
                    borderBottom: "solid 1px",
                    borderColor: "#d9d9d9",
                  }}
                  key={person.id}
                >
                  <Link
                    style={{
                      width: 230,
                      height: 35,
                      paddingLeft: 15,
                      display: "flex",
                      alignItems: "center",
                      textDecoration: "none",
                      color: person.isDeleted ? "white" : "black",
                      backgroundColor: person.isDeleted ? "#DA5451" : "white",
                    }}
                    to={`/consultants/${person.id}`}
                  >
                    {person.lastName} {person.firstName}
                  </Link>
                  <Button
                    danger={person.isDeleted ? false : true}
                    icon={
                      person.isDeleted ? (
                        <ToolOutlined className="restore" />
                      ) : (
                        <DeleteOutlined />
                      )
                    }
                    style={{
                      borderWidth: 0,
                      borderRadius: 0,
                      height: 35,
                      width: 35,
                      borderLeft: "solid 1px #d9d9d9",
                    }}
                    onClick={() =>
                      person.isDeleted
                        ? handleRestore(person.id)
                        : handleDelete(person.id)
                    }
                  ></Button>
                </div>
              ))
          ) : (
            <Spin
              delay={100}
              style={{
                display: "flex",
                justifyContent: "center",
                paddingTop: 25,
              }}
              indicator={
                <Loading3QuartersOutlined
                  style={{ fontSize: 24 }} //color: "#2397D4"
                  spin
                />
              }
            ></Spin>
          )}
        </div>
      </div>
    </div>
  );
};

export default PersonSearchBar;
