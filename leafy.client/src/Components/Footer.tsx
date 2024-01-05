import '../assets/footer.css';
import logo from '../Images/calendula.png';
import { Link } from 'react-router-dom';


function Footer() {
  return (
    <footer className="footer">
      <div className="container">
        <div className="footer-content">
          <div className="footer-logo"><img className="footer-logo-img" src={logo}/></div>
          <div className="footer-links">
            <ul className="footer-menu">
              <li>
                <Link to='/' >Home</Link>
              </li>
              <li>
                <Link to="/about">About Us</Link>
              </li>
            </ul>
          </div>
          </div>
        </div>
      <div className="footer-bottom">
            <p>&copy; 2023 Leafy. All rights reserved.</p>
          </div>
    </footer>
  );
}

export default Footer;