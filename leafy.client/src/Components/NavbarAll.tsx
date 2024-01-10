//import "../assets/bootstrap.min.css";
import { useState } from "react";
import logo from "../Images/calendula.gif";
import Offcanvas from "react-bootstrap/Offcanvas";
import menuicon from "../Images/menu50.png";
import { OffcanvasBody } from "react-bootstrap";
import fontadi from "../assets/fonts/LibreBaskerville-Bold.ttf";
import { Link } from "react-router-dom";
import close from '../Images/icons8-close-100.png';
import axios from "axios";



function Navbar() {
  //show hide offcanvas
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  //

  return (
    <>
      <nav
        className="navbar navbar-expand-md mt-auto py-3"
              style={{ background: "#12391E"}}
      >
        <div className="container">
          <div style={{  marginLeft: "0px", marginRight: "px" }}>
            <Link to="/" replace><img src={logo} style={{width: "64", height: "64", background: "transparent"}}/></Link>   
          </div>
                  <span style={{ color: "#F5DEB3", fontSize: "42px", fontFamily: 'fontadi' }}>Leafy</span>
         <button 
            onClick={handleShow}
             style={{ background: "transparent", border: "none", padding: "0px", transform:"scale(1.5)"}}
          >
            <img src={menuicon} alt="" />
          </button>
        </div>
      </nav>
      <Offcanvas show={show} onHide={handleClose}>
              <div className="offcanvas-header" style={{background:"#F5DEB3" }}>
          <Link to="/" replace={true}
            className="link-body-emphasis d-flex align-items-center me-md-auto mb-3 mb-md-0 text-decoration-none"
          >
            <img src={logo} alt="" style={{width: "48", height: "48", background: "transparent"}}/>
                      <span className="fs-4" style={{ fontFamily: 'fontadi' }}>Leafy</span>
                  </Link>
                  <button className="btn-close" style={{ background: "transparent", height: "30", width:"30" }}
            type="button"
            aria-label="Close"
            data-bs-dismiss="offcanvas"
            onClick={handleClose}
             
          ><img src={close} alt="" /></button>
        </div>

              <OffcanvasBody style={{ background: '#12391E'}}>

                  <div style={{ background: '#12391E', height: 'auto'}}>
          <hr className="mt-0" />
          <ul className="nav nav-pills flex-column mb-auto">
            <li className="nav-item">
              <Link to="/" replace={true}
                className="nav-link"
                     style={{ color: "#F5DEB3"}}
              >
                <svg
                  className="bi bi-speedometer2 me-2"
                  xmlns="http://www.w3.org/2000/svg"
                  width="1em"
                  height="1em"
                  fill="currentColor"
                  viewBox="0 0 16 16"
                >
                  <path d="M8 4a.5.5 0 0 1 .5.5V6a.5.5 0 0 1-1 0V4.5A.5.5 0 0 1 8 4zM3.732 5.732a.5.5 0 0 1 .707 0l.915.914a.5.5 0 1 1-.708.708l-.914-.915a.5.5 0 0 1 0-.707zM2 10a.5.5 0 0 1 .5-.5h1.586a.5.5 0 0 1 0 1H2.5A.5.5 0 0 1 2 10zm9.5 0a.5.5 0 0 1 .5-.5h1.5a.5.5 0 0 1 0 1H12a.5.5 0 0 1-.5-.5zm.754-4.246a.389.389 0 0 0-.527-.02L7.547 9.31a.91.91 0 1 0 1.302 1.258l3.434-4.297a.389.389 0 0 0-.029-.518z"></path>
                  <path
                    d="M0 10a8 8 0 1 1 15.547 2.661c-.442 1.253-1.845 1.602-2.932 1.25C11.309 13.488 9.475 13 8 13c-1.474 0-3.31.488-4.615.911-1.087.352-2.49.003-2.932-1.25A7.988 7.988 0 0 1 0 10zm8-7a7 7 0 0 0-6.603 9.329c.203.575.923.876 1.68.63C4.397 12.533 6.358 12 8 12s3.604.532 4.923.96c.757.245 1.477-.056 1.68-.631A7 7 0 0 0 8 3z"
                  ></path>
                </svg>
                Home
              </Link>
            </li>
            
            <li className="nav-item">
              <Link to="/signup" replace={true}
                className="nav-link"
                   style={{ color: "#F5DEB3"}}
              >
                <svg
                  className="bi bi-grid me-2"
                  xmlns="http://www.w3.org/2000/svg"
                  width="1em"
                  height="1em"
                  fill="currentColor"
                  viewBox="0 0 16 16"
                >
                  <path d="M1 2.5A1.5 1.5 0 0 1 2.5 1h3A1.5 1.5 0 0 1 7 2.5v3A1.5 1.5 0 0 1 5.5 7h-3A1.5 1.5 0 0 1 1 5.5v-3zM2.5 2a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3zm6.5.5A1.5 1.5 0 0 1 10.5 1h3A1.5 1.5 0 0 1 15 2.5v3A1.5 1.5 0 0 1 13.5 7h-3A1.5 1.5 0 0 1 9 5.5v-3zm1.5-.5a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3zM1 10.5A1.5 1.5 0 0 1 2.5 9h3A1.5 1.5 0 0 1 7 10.5v3A1.5 1.5 0 0 1 5.5 15h-3A1.5 1.5 0 0 1 1 13.5v-3zm1.5-.5a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3zm6.5.5A1.5 1.5 0 0 1 10.5 9h3a1.5 1.5 0 0 1 1.5 1.5v3a1.5 1.5 0 0 1-1.5 1.5h-3A1.5 1.5 0 0 1 9 13.5v-3zm1.5-.5a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3z"></path>
                </svg>{" "}
                Üye ol{" "}
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/login" replace={true}
                className="nav-link"
                   style={{ color: "#F5DEB3"}}
              >
                <svg
                  className="bi bi-grid me-2"
                  xmlns="http://www.w3.org/2000/svg"
                  width="1em"
                  height="1em"
                  fill="currentColor"
                  viewBox="0 0 16 16"
                >
                  <path d="M1 2.5A1.5 1.5 0 0 1 2.5 1h3A1.5 1.5 0 0 1 7 2.5v3A1.5 1.5 0 0 1 5.5 7h-3A1.5 1.5 0 0 1 1 5.5v-3zM2.5 2a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3zm6.5.5A1.5 1.5 0 0 1 10.5 1h3A1.5 1.5 0 0 1 15 2.5v3A1.5 1.5 0 0 1 13.5 7h-3A1.5 1.5 0 0 1 9 5.5v-3zm1.5-.5a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3zM1 10.5A1.5 1.5 0 0 1 2.5 9h3A1.5 1.5 0 0 1 7 10.5v3A1.5 1.5 0 0 1 5.5 15h-3A1.5 1.5 0 0 1 1 13.5v-3zm1.5-.5a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3zm6.5.5A1.5 1.5 0 0 1 10.5 9h3a1.5 1.5 0 0 1 1.5 1.5v3a1.5 1.5 0 0 1-1.5 1.5h-3A1.5 1.5 0 0 1 9 13.5v-3zm1.5-.5a.5.5 0 0 0-.5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 0-.5-.5h-3z"></path>
                </svg>{" "}
                Giriş Yap{" "}
              </Link>
            </li>

          </ul>
        </div>
        </OffcanvasBody>
      </Offcanvas>
    </>
  );
}

export default Navbar;
