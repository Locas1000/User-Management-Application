import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';

function Register() {
    const [formData, setFormData] = useState({ name: '', email: '', password: '' });
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        try {
            // NOTE: We use the port 5013 (HTTP) that worked for you earlier
            const response = await fetch('http://localhost:5013/api/auth/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.message || 'Registration failed');
            }

            // If successful, go to login page
            alert('Registration successful! Please log in.');
            navigate('/login');
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className="container mt-5" style={{ maxWidth: '400px' }}>
            <div className="card shadow">
                <div className="card-body">
                    <h2 className="text-center mb-4">Register</h2>
                    {error && <div className="alert alert-danger">{error}</div>}

                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label className="form-label">Name</label>
                            <input
                                type="text" className="form-control" name="name"
                                onChange={handleChange} required
                            />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Email</label>
                            <input
                                type="email" className="form-control" name="email"
                                onChange={handleChange} required
                            />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Password</label>
                            <input
                                type="password" className="form-control" name="password"
                                onChange={handleChange} required
                            />
                        </div>
                        <button type="submit" className="btn btn-primary w-100">Sign Up</button>
                    </form>
                    <div className="text-center mt-3">
                        <Link to="/login">Already have an account? Login</Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Register;