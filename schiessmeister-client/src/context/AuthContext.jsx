import { createContext, useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
	const [token, setToken] = useState(localStorage.getItem('token'));
	const [userId, setUserId] = useState(localStorage.getItem('userId'));
	const navigate = useNavigate();

	useEffect(() => {
		if (token) {
			localStorage.setItem('token', token);
		} else {
			localStorage.removeItem('token');
		}
	}, [token]);

	useEffect(() => {
		if (userId) {
			localStorage.setItem('userId', userId);
		} else {
			localStorage.removeItem('userId');
		}
	}, [userId]);

	const login = (newToken, newUserId) => {
		setToken(newToken);
		setUserId(newUserId);
		navigate('/home');
	};

	const logout = () => {
		setToken(null);
		setUserId(null);
		localStorage.removeItem('token');
		localStorage.removeItem('userId');
		navigate('/login');
	};

	const handleUnauthorized = () => {
		logout();
	};

	return <AuthContext.Provider value={{ token, userId, login, logout, handleUnauthorized }}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
	const context = useContext(AuthContext);
	if (!context) {
		throw new Error('useAuth must be used within an AuthProvider');
	}
	return context;
};
