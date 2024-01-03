import '../assets/Login.css';
import axios from 'axios';

import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBInput,
}
from 'mdb-react-ui-kit';

function Login() {
  var email = '';
  var password = '';
  const login = async () => {
    await axios.post(`api/Auth/loginJWT`,  {
      email,
      password
    })
    .then(response => {
      console.log(response);
      window.sessionStorage.setItem('token', "Bearer "+response.data.accessToken);
    }).catch(error => {
      console.log(error);
    });
    console.log(window.sessionStorage.getItem('token'));
    }

    const getEmail = () => {
        email = (document.getElementById('email') as HTMLInputElement).value;
        console.log(email);
    }

    const getPassword = () => {
        password = (document.getElementById('password') as HTMLInputElement).value;
        console.log(password);
    }

  return (
    <div className='login'>
    <MDBContainer fluid>

      <MDBRow className='d-flex justify-content-center align-items-center h-100'>
        <MDBCol col='12'>

          <MDBCard className='bg-dark text-white my-5 mx-auto' style={{borderRadius: '1rem', maxWidth: '400px'}}>
            <MDBCardBody className='p-5 d-flex flex-column align-items-center mx-auto w-100'>

              <h2 className="fw-bold mb-2 text-uppercase">Login</h2>
              <p className="text-white-50 mb-5">Please enter your login and password!</p>
                          <div id='formControlLg' style={{ paddingRight: 100 }}>
                              <MDBInput id='email' onChange={getEmail} wrapperClass='mb-4 mx-5 w-100' labelClass='text-white' label='email' type='email' size="lg" />
                              <MDBInput id='password' onChange={getPassword} wrapperClass='mb-4 mx-5 w-100' labelClass='text-white' label='password' type='password' size="lg" />
              </div>
                          <p className="small mb-3 pb-lg-2"><a className="text-white-50" href="#!">Forgot password?</a></p>
                          <button id='loginbtn' className="btnLogin" onClick={login} color='white'>
                Login
              </button>

              <div>
                <p  className="mb-0">Don't have an account? <a href="#!" className="text-white-50 fw-bold">Sign Up</a></p>

              </div>
            </MDBCardBody>
          </MDBCard>

        </MDBCol>
      </MDBRow>

    </MDBContainer>
    </div>
  );
}

export default Login;