//import './App.css';
import './assets/bootstrap.min.css';
import './assets/styles.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './Pages/Home';
import About from './Pages/AboutUs';
import Nav from './Components/Navbar';
import Login from './Pages/Login';
import Footer from './Components/Footer';
import User from './Pages/User';
import Signup from './Pages/Signup';



function App() {
    
    return <div>
        <Router>
            <Nav />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={< Login />} />
                <Route path="/about" element={<About />} />
                <Route path="/users"  element={<User/>}  />
                <Route path='/signup' element={<Signup/>}/>
            </Routes>
            <Footer />
        </Router>
    </div>
}

export default App;