import { createApi } from '../utils/api';

export const loginRequest = async (username, password) => {
    const api = createApi();
    return api.post('/authenticate/login', { username, password });
};

export const registerRequest = async (username, email, password) => {
    const api = createApi();
    return api.post('/authenticate/register', { username, email, password });
};

export const getSubscriptionDetails = async (competitionId) => {
    const api = createApi();
    return api.get(`/competition/${competitionId}/subscribe`);
};
