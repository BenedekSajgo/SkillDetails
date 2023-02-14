import { Collapse, Spin } from "antd";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { PersonController } from "../Axios/PersonController";
import { Service } from "../Axios/Service";
import { PersonDetailInterface } from "../Interfaces/PersonDetailInterface";
import EducationPanel from "./PersonDetailPanels/EducationPanel";
import IdentityPanel from "./PersonDetailPanels/IdentityPanel";
import ProjectPanel from "./PersonDetailPanels/ProjectPanel";
import SkillPanel from "./PersonDetailPanels/SkillPanel";

const PersonDetail = () => {
  //const { id } = useParams();
  const { id } = useParams();
  console.log(id);

  const [isActive, setIsActive] = useState<boolean>(false);
  const [person, setPerson] = useState<Service<PersonDetailInterface>>({
    status: "loading",
  });

  useEffect(() => {
    id === undefined
      ? setIsActive(false)
      : PersonController.getPerson(parseInt(id!)).then((data) => {
          setIsActive(true);
          setPerson(data);
          console.log(data);
        });
  }, [id]);

  return (
    <div
      style={{
        minWidth: 750,
      }}
    >
      {isActive ? (
        person.status === "loaded" ? (
          <Collapse
            defaultActiveKey={[1, 2, 3, 4]}
            style={{ borderRadius: 0, borderLeft: 0, borderTop: 0 }}
          >
            <Collapse.Panel key={1} header="Identity">
              <IdentityPanel {...person.payload} />
            </Collapse.Panel>
            <Collapse.Panel key={2} header="Skills">
              <SkillPanel />
            </Collapse.Panel>
            <Collapse.Panel key={3} header="Projects">
              <ProjectPanel />
            </Collapse.Panel>
            <Collapse.Panel
              key={4}
              header="Educations"
              style={{ borderRadius: 0 }}
            >
              <EducationPanel />
            </Collapse.Panel>
          </Collapse>
        ) : (
          <Spin delay={100} spinning={false}>
            Loading
          </Spin>
        )
      ) : (
        <Spin delay={100} spinning={false}>
          No user selected
        </Spin>
      )}
    </div>
  );
};

export default PersonDetail;
