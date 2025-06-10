import { createContext, useContext, useState } from 'react';
import { sampleCompetitions } from '../data/sampleData';

const DataContext = createContext(null);

export const DataProvider = ({ children }) => {
  const [competitions, setCompetitions] = useState(sampleCompetitions);

  const addCompetition = (competition) => {
    setCompetitions([...competitions, { ...competition, id: Date.now() }]);
  };

  const updateCompetition = (id, updated) => {
    setCompetitions(
      competitions.map((c) => (c.id === id ? { ...c, ...updated } : c))
    );
  };

  return (
    <DataContext.Provider value={{ competitions, addCompetition, updateCompetition }}>
      {children}
    </DataContext.Provider>
  );
};

export const useData = () => {
  const ctx = useContext(DataContext);
  if (!ctx) throw new Error('useData must be used within DataProvider');
  return ctx;
};
