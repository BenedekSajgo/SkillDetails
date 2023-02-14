import { PersonDetailInterface } from "../../Interfaces/PersonDetailInterface";

const IdentityPanel = (props: PersonDetailInterface) => {
  return (
    <div>
      {props.firstName} {props.lastName}
    </div>
  );
};

export default IdentityPanel;
