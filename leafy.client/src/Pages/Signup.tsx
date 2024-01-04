import "../assets/Login.css";
import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBInput,
} from "mdb-react-ui-kit";
import { Link } from "react-router-dom";

//variables

var email = "";
var password = "";
var confirmPassword = "";
var username = "";




const getPassword = () => {
  password = (document.getElementById("password") as HTMLInputElement).value;
  console.log(password);
};

const getConfirmPassword = () => {
  confirmPassword = (
    document.getElementById("confirmPassword") as HTMLInputElement
).value;
  console.log(confirmPassword);
};

const getUsername = () => {
  username = (document.getElementById("username") as HTMLInputElement).value;
  console.log(username);
};

const getEmail = () => {
  email = (document.getElementById("email") as HTMLInputElement).value;
};

const emailRegex = (mail: string) => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(mail);
};

function controller() {
  console.log(username);
  console.log(email);
  console.log(password);
  console.log(confirmPassword);
  if (email == "" || password == "" || confirmPassword == "" || username == "") {
    alert('Lütfen boş form bırakmayıınız.')
  }
  else if(emailRegex(email) == false){
    alert('Lütfen geçerli bir email adresi giriniz.')
  }
  else if(password.length < 6 || password.length > 14) {
    alert('Şifre 6 ila 14 karakterden oluşmalıdır.')
  }
  else if(password != confirmPassword){
    alert('Lütfen şifrelerin aynı olduğundan emin olunur.')
  }
  else(
    alert('başarılı')
  )


}


function Signup() {
  return (
    <div>
      <MDBContainer fluid>
        <MDBRow className="d-flex justify-content-center align-items-center h-100">
          <MDBCol col="12">
            <MDBCard
              className="bg-dark text-white my-5 mx-auto"
              style={{ borderRadius: "1rem", maxWidth: "400px" }}
            >
              <MDBCardBody className="p-5 d-flex flex-column align-items-center mx-auto w-100">
                <h2 className="fw-bold mb-2 text-uppercase">Sign Up</h2>
                <p className="text-white-50 mb-5">
                  Please enter information for sign up!
                </p>
                <div id="formControlLg" style={{ paddingRight: 100 }}>
                  <MDBInput
                    id="username"
                    onChange={getUsername}
                    wrapperClass="mb-4 mx-5 w-100"
                    labelClass="text-white"
                    label="Username"
                    type="text"
                    size="lg"
                  />
                  <MDBInput
                    id="email"
                    onChange={getEmail}
                    wrapperClass="mb-4 mx-5 w-100"
                    labelClass="text-white"
                    label="E-mail"
                    type="email"
                    size="lg"
                  />
                  
                  <MDBInput
                    id="password"
                    onChange={getPassword}
                    wrapperClass="mb-4 mx-5 w-100"
                    labelClass="text-white"
                    label="Password"
                    type="password"
                    size="lg"
                  />

                  <MDBInput
                    id="confirmPassword"
                    onChange={getConfirmPassword}
                    wrapperClass="mb-4 mx-5 w-100"
                    labelClass="text-white"
                    label="Confirm Password"
                    type="password"
                    size="lg"
                  />
                </div>
                <p className="small mb-3 pb-lg-2">
                  <Link to={"/login"} className="text-white-50">
                    Already signed up?
                  </Link>
                </p>
                <button
                  id="loginbtn"
                  className="btnLogin"
                  onClick={controller}
                  color="white"
                >
                  Sing Up
                </button>
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
        </MDBRow>
      </MDBContainer>
    </div>
  );
}

export default Signup;
