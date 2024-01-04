//import './App.css';
import './assets/bootstrap.min.css';
import './assets/styles.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './Pages/Home';
import About from './Pages/AboutUs';
import Nav from './Components/Navbar';
import Login from './Pages/Login';
import Footer from './Components/Footer';





function App() {
    return <div>
        <Router>
            <Nav />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={< Login />} />
                <Route path="/About" element={<About />} />
            </Routes>
            <Footer />
        </Router>
    </div>
}

export default App;