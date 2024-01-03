import '../assets/footer.css';
import logo from '../Images/calendula.png';


function Footer() {
  return (
    <footer className="footer">
      <div className="container">
        <div className="footer-content">
          <div className="footer-logo"><img className="footer-logo-img" src={logo}/></div>
          <div className="footer-links">
            <ul className="footer-menu">
              <li>
                <a href="#">Home</a>
              </li>
              <li>
                <a href="#">About</a>
              </li>
              <li>
                <a href="#">Services</a>
              </li>
              <li>
                <a href="#">Contact</a>
              </li>
            </ul>
          </div>
        </div>
        <div className="footer-bottom">
          <p>&copy; 2023 Leafy. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}

export default Footer;