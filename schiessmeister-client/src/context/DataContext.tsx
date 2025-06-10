import { createContext, useContext, useState } from 'react'
import { sampleCompetitions } from '../data/sampleData'
import type { Competition } from '../types'

interface DataContextType {
  competitions: Competition[]
  addCompetition: (competition: Competition) => void
  updateCompetition: (id: number, updated: Partial<Competition>) => void
}

const DataContext = createContext<DataContextType | undefined>(undefined)

export const DataProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [competitions, setCompetitions] = useState<Competition[]>(sampleCompetitions)

  const addCompetition = (competition: Competition) => {
    setCompetitions([...competitions, { ...competition, id: Date.now() }])
  };

  const updateCompetition = (id: number, updated: Partial<Competition>) => {
    setCompetitions(
      competitions.map((c) => (c.id === id ? { ...c, ...updated } : c))
    )
  };

  return (
    <DataContext.Provider value={{ competitions, addCompetition, updateCompetition }}>
      {children}
    </DataContext.Provider>
  );
};

export const useData = () => {
  const ctx = useContext(DataContext)
  if (!ctx) throw new Error('useData must be used within DataProvider')
  return ctx
};
