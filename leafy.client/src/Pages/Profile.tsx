import axios from "axios";
import { useEffect, useState } from "react";
import {
  MDBCard,
  MDBCardBody,
  MDBCardImage,
  MDBCol,
  MDBContainer,
  MDBIcon,
  MDBRow,
  MDBTypography,
  MDBCardText,
} from "mdb-react-ui-kit";
import "../assets/profile.css";
import "../assets/bootstrap.min.css";
import profilephoto from "../Images/groot.jpeg";
import desert from "../Images/desert.jpg";

interface User {
  userName: string;
  userMail: string;
  userRole: string;
  registeredDate: string;
}

function Profile() {
  const [user, setUser] = useState<User>({
    userName: "",
    userMail: "",
    userRole: "",
    registeredDate: "",
  });

  useEffect(() => {
    const getUser = async () => {
      try {
        const response = await axios.post("api/Auth/getStatus");
        setUser(response.data);
      } catch (error) {
        console.log(error);
      }
    };

    getUser();
  }, []);

  return (
    <div className="main_div" >
      <div className="container bootstrap snippets bootdey">
        <div className="rowinfo">
          <div className="col-md-3">
            <div className="text-center">
              <img
                src={profilephoto}
                className="avatar img-circle img-thumbnail"
                alt="avatar"
                />
            </div>
          </div>

          <div className="col-md-9 personal-info">
            <h3>Personal info</h3>

            <form className="form-horizontal" role="form">
              <div className="form-group">
                <label className="col-lg-3 control-label">İsim:</label>
                <div className="col-lg-8">
                  <input className="form-control" type="text" value={user.userName} readOnly={true} />
                </div>
              </div>
              <div className="form-group">
                <label className="col-lg-3 control-label">E-mail:</label>
                <div className="col-lg-8">
                  <input readOnly={true}
                    className="form-control"
                    type="text"
                    value={user.userMail}
                  />
                </div>
              </div>
              <div className="form-group">
                <label className="col-lg-3 control-label">Kayıt tarihi:</label>
                <div className="col-lg-8">
                  <input className="form-control" type="text" value={user.registeredDate} readOnly={true} />
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
      <hr />
    </div>
  );
}

export default Profile;
