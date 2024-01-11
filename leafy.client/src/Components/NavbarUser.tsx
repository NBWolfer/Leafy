//import "../assets/bootstrap.min.css";
import { useState } from "react";
import logo from "../Images/calendula.gif";
import Offcanvas from "react-bootstrap/Offcanvas";
import menuicon from "../Images/menu50.png";
import { OffcanvasBody } from "react-bootstrap";
import fontadi from "../assets/fonts/LibreBaskerville-Bold.ttf";
import { Link } from "react-router-dom";
import close from "../Images/icons8-close-100.png";
import axios from "axios";
import treeicom from "../Images/tree.png";
import logouticon from "../Images/logout.png";

async function logout() {
  await axios
    .post(`api/Auth/logout`)
    .then((response) => {
      console.log(response);
      window.location.href = '/';
    })
    .catch((error) => {
      console.log(error);
    });
}

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
        style={{ background: "#12391E" }}
      >
        <div className="container">
          <div style={{ marginLeft: "0px", marginRight: "px" }}>
            <Link to="/" replace>
              <img
                src={logo}
                style={{ width: "64", height: "64", background: "transparent" }}
              />
            </Link>
          </div>
          <span
            style={{
              color: "#F5DEB3",
              fontSize: "42px",
              fontFamily: "fontadi",
            }}
          >
            Leafy
          </span>
          <button
            onClick={handleShow}
            style={{
              background: "transparent",
              border: "none",
              padding: "0px",
              transform: "scale(1.5)",
            }}
          >
            <img src={menuicon} alt="" />
          </button>
        </div>
      </nav>
      <Offcanvas show={show} onHide={handleClose}>
        <div className="offcanvas-header" style={{ background: "#F5DEB3" }}>
          <Link
            to="/"
            replace={true}
            className="link-body-emphasis d-flex align-items-center me-md-auto mb-3 mb-md-0 text-decoration-none"
          >
            <img
              src={logo}
              alt=""
              style={{ width: "48", height: "48", background: "transparent" }}
            />
            <span className="fs-4" style={{ fontFamily: "fontadi" }}>
              Leafy
            </span>
          </Link>
          <button
            className="btn-close"
            style={{ background: "transparent", height: "30", width: "30" }}
            type="button"
            aria-label="Close"
            data-bs-dismiss="offcanvas"
            onClick={handleClose}
          >
            <img src={close} alt="" />
          </button>
        </div>

        <OffcanvasBody style={{ background: "#12391E" }}>
          <div style={{ background: "#12391E", height: "auto" }}>
            <hr className="mt-0" />
            <ul className="nav nav-pills flex-column mb-auto">
              <li className="nav-item">
                <Link
                  to="/"
                  replace={true}
                  className="nav-link"
                  style={{ color: "#F5DEB3" }}
                >
                  <img
              src={treeicom}
              alt=""
              style={{ width: "1.5rem", height: "1.5rem", background: "transparent" }}
            />
                  {"  "} Home
                </Link>
              </li>

              <li className="nav-item">
                <Link
                  to="/scanplant"
                  replace={true}
                  className="nav-link"
                  style={{ color: "#F5DEB3" }}
                >
                  <svg
                    className="bi bi-calendar-plus me-2"
                    xmlns="http://www.w3.org/2000/svg"
                    width="1em"
                    height="1em"
                    fill="currentColor"
                    viewBox="0 0 16 16"
                  >
                    <path d="M8 7a.5.5 0 0 1 .5.5V9H10a.5.5 0 0 1 0 1H8.5v1.5a.5.5 0 0 1-1 0V10H6a.5.5 0 0 1 0-1h1.5V7.5A.5.5 0 0 1 8 7z"></path>
                    <path d="M3.5 0a.5.5 0 0 1 .5.5V1h8V.5a.5.5 0 0 1 1 0V1h1a2 2 0 0 1 2 2v11a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3a2 2 0 0 1 2-2h1V.5a.5.5 0 0 1 .5-.5zM1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4H1z"></path>
                  </svg>
                  Bitki Tara
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  to="/Plantss"
                  replace={true}
                  className="nav-link"
                  style={{ color: "#F5DEB3" }}
                >
                  <svg
                    className="bi bi-people me-2"
                    xmlns="http://www.w3.org/2000/svg"
                    width="1em"
                    height="1em"
                    fill="currentColor"
                    viewBox="0 0 16 16"
                  >
                    <path d="M15 14s1 0 1-1-1-4-5-4-5 3-5 4 1 1 1 1h8Zm-7.978-1A.261.261 0 0 1 7 12.996c.001-.264.167-1.03.76-1.72C8.312 10.629 9.282 10 11 10c1.717 0 2.687.63 3.24 1.276.593.69.758 1.457.76 1.72l-.008.002a.274.274 0 0 1-.014.002H7.022ZM11 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4Zm3-2a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM6.936 9.28a5.88 5.88 0 0 0-1.23-.247A7.35 7.35 0 0 0 5 9c-4 0-5 3-5 4 0 .667.333 1 1 1h4.216A2.238 2.238 0 0 1 5 13c0-1.01.377-2.042 1.09-2.904.243-.294.526-.569.846-.816ZM4.92 10A5.493 5.493 0 0 0 4 13H1c0-.26.164-1.03.76-1.724.545-.636 1.492-1.256 3.16-1.275ZM1.5 5.5a3 3 0 1 1 6 0 3 3 0 0 1-6 0Zm3-2a2 2 0 1 0 0 4 2 2 0 0 0 0-4Z"></path>
                  </svg>{" "}
                  Bitkilerim
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  to="/profile"
                  replace={true}
                  className="nav-link"
                  style={{ color: "#F5DEB3" }}
                >
                  <svg
                    className="bi bi-people me-2"
                    xmlns="http://www.w3.org/2000/svg"
                    width="1em"
                    height="1em"
                    fill="currentColor"
                    viewBox="0 0 16 16"
                  >
                    <path d="M15 14s1 0 1-1-1-4-5-4-5 3-5 4 1 1 1 1h8Zm-7.978-1A.261.261 0 0 1 7 12.996c.001-.264.167-1.03.76-1.72C8.312 10.629 9.282 10 11 10c1.717 0 2.687.63 3.24 1.276.593.69.758 1.457.76 1.72l-.008.002a.274.274 0 0 1-.014.002H7.022ZM11 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4Zm3-2a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM6.936 9.28a5.88 5.88 0 0 0-1.23-.247A7.35 7.35 0 0 0 5 9c-4 0-5 3-5 4 0 .667.333 1 1 1h4.216A2.238 2.238 0 0 1 5 13c0-1.01.377-2.042 1.09-2.904.243-.294.526-.569.846-.816ZM4.92 10A5.493 5.493 0 0 0 4 13H1c0-.26.164-1.03.76-1.724.545-.636 1.492-1.256 3.16-1.275ZM1.5 5.5a3 3 0 1 1 6 0 3 3 0 0 1-6 0Zm3-2a2 2 0 1 0 0 4 2 2 0 0 0 0-4Z"></path>
                  </svg>{" "}
                  Profilim
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  to="/"
                  replace={true}
                  onClick={logout}
                  className="nav-link"
                  style={{ color: "#F5DEB3" }}
                >
                  <img
              src={logouticon}
              alt=""
              style={{ width: "1.5rem", height:"1.5rem", background: "transparent" }}
            />
                  Çıkış yap
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
