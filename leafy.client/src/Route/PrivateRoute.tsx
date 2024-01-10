import { Navigate, Route, Routes } from 'react-router-dom';
import axios from 'axios';
import Scanplant from '../Pages/Scanplant';
import User from '../Pages/User';
import Home from '../Pages/Home';
import About from '../Pages/AboutUs';
import { lazy } from 'react';
const Plantss = lazy(() => import('../Pages/Plantss'));



export const PrivateRoutes = () => {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/user"  element={<User/>}  />
            <Route path='/scanplant' element={<Scanplant />} />
            <Route path='/plantss' element={<Plantss />} />
            <Route path='*'element={<Navigate state={{ from: '*' }} to={'/'}/>}/>
        </Routes>
    );
};