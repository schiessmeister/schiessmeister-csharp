import { Link } from 'react-router-dom';
import { useData } from '../context/DataContext';
import { Button } from '@/components/ui/button';

const Competitions = () => {
  const { competitions } = useData();

  return (
    <main>
      <h2>Wettbewerbe</h2>
      <div className="comp-list">
        {competitions.map((c) => (
          <Link key={c.id} to={`/competitions/${c.id}`} className="comp-item">
            {c.name}
          </Link>
        ))}
        {competitions.length === 0 && <p>Keine Wettbewerbe vorhanden.</p>}
      </div>
      <Link to="/competitions/new">
        <Button>Neuer Wettbewerb</Button>
      </Link>
    </main>
  );
};

export default Competitions;
