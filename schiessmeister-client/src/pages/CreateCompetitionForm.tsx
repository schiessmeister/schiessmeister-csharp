import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useData } from '../context/DataContext';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';

interface FormProps {
  initial?: Record<string, string>
  onSave: (comp: Record<string, string>) => void
}

const CreateCompetitionForm: React.FC<FormProps> = ({ initial = {}, onSave }) => {
  const [name, setName] = useState(initial.name || '');
  const [location, setLocation] = useState(initial.location || '');
  const [date, setDate] = useState(initial.date || '');

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    onSave({ name, location, date });
  };

  return (
    <form onSubmit={handleSubmit} className="form-grid">
      <Input value={name} onChange={(e) => setName(e.target.value)} placeholder="Name" required />
      <Input value={location} onChange={(e) => setLocation(e.target.value)} placeholder="Ort" required />
      <Input type="datetime-local" value={date} onChange={(e) => setDate(e.target.value)} required />
      <Button type="submit">Speichern</Button>
    </form>
  );
};

export default CreateCompetitionForm;
