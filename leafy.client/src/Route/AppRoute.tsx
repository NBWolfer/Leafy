import React, { useEffect, useState } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { PrivateRoutes } from './PrivateRoute';
import { PublicRoutes } from './PublicRoute';
import axios from 'axios';
import Footer from '../Components/Footer';
import NavbarAll from '../Components/NavbarAll';
import NavbarUser from '../Components/NavbarUser';

type Status = 'checking' | 'authenticated' | 'no-authenticated';

export const AppRouter = () => {
  const [status, setStatus] = useState<Status>('checking');

  useEffect(() => {
    const checkAuth = async () => {
      try {
        const response = await axios.post('api/Auth/getStatus');
        setStatus(response.data.status ? 'authenticated' : 'no-authenticated');
      } catch (error) {
        console.log(error);
      }
    };

    checkAuth();
  }, []); 

 
  if (status === 'checking') return <div className="loading">Checking credentials...</div>;

  return (
    <BrowserRouter>
        {status === 'authenticated' ? <NavbarUser/> : <NavbarAll/>}
      <Routes>
        {status === 'authenticated' ? ( 
          <Route path="/*" element={<PrivateRoutes />} />
        ) : (
          <Route path="/*" element={<PublicRoutes />} />
        )}

        <Route path="*" element={<Navigate to="/login" replace />} />
      
      </Routes>
        <Footer></Footer>
    </BrowserRouter>
  );
};
