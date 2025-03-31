import { createContext, useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
	const [token, setToken] = useState(localStorage.getItem('token'));
	const navigate = useNavigate();

	useEffect(() => {
		if (token) {
			localStorage.setItem('token', token);
		} else {
			localStorage.removeItem('token');
		}
	}, [token]);

	const login = (newToken) => {
		setToken(newToken);
		navigate('/home');
	};

	const logout = () => {
		setToken(null);
		localStorage.removeItem('token');
		navigate('/login');
	};

	const handleUnauthorized = () => {
		logout();
	};

	return <AuthContext.Provider value={{ token, login, logout, handleUnauthorized }}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
	const context = useContext(AuthContext);
	if (!context) {
		throw new Error('useAuth must be used within an AuthProvider');
	}
	return context;
};
