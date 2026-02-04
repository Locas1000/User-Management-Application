import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Register from './components/Register';
import Login from './components/Login';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Default route: Redirect to Login */}
                <Route path="/" element={<Navigate to="/login" />} />

                {/* Our Pages */}
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />

                {/* Placeholder for the table we will build next */}
                <Route path="/users" element={<h2>User Management Table Coming Soon...</h2>} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;