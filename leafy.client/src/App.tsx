//import './App.css';
import './assets/bootstrap.min.css';
import './assets/styles.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './Pages/Home';
import Login from './Pages/Login';
import Nav from './Components/Navbar';





function App() {
    return <div>
        <Router>
            <Nav />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
            </Routes>
        </Router>
        </div>
}

export default App;