import { Navigate, Route, Routes } from 'react-router-dom';
import Login  from '../Pages/Login';
import Signup from '../Pages/Signup';
import Home from '../Pages/Home';
import AboutUs from '../Pages/AboutUs';

export const PublicRoutes = () => {
    return ( 
        <Routes>

            <Route path='about' element={<AboutUs />} />
            <Route path='login' element={<Login />} />
            <Route path='signup' element={<Signup />} />
            <Route path='/'  element={<Home />} />
            <Route path='*'element={<Navigate state={{ from: '*' }} to={'/login'}/>}/>
        </Routes>
    );
};