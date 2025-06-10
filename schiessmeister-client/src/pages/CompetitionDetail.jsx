import { Link, useParams } from 'react-router-dom';
import { useData } from '../context/DataContext';
import { Button } from '@/components/ui/button';
import { TreeView } from '@/components/tree-view';
import { Dialog, DialogTrigger, DialogContent, DialogHeader, DialogTitle, DialogFooter, DialogClose } from '@/components/ui/dialog';
import { useState } from 'react';
import { Input } from '@/components/ui/input';
import { Pencil, Folder, File } from 'lucide-react';

const KlassenList = ({ klassen, onRemove }) => (
  <div>
    <label className="block font-medium mb-1">Klassen</label>
    <ul>
      {klassen.map((k, i) => (
        <li key={i} className="flex items-center gap-2 mb-1">
          <span>{k}</span>
          {onRemove && <Button type="button" size="icon" variant="ghost" onClick={() => onRemove(i)}>🗑️</Button>}
        </li>
      ))}
    </ul>
  </div>
);

const SchreiberList = ({ schreiber, onRemove }) => (
  <div>
    <label className="block font-medium mb-1">Schreiber</label>
    <ul>
      {schreiber.map((s, i) => (
        <li key={i} className="flex items-center gap-2 mb-1">
          <span>{s}</span>
          {onRemove && <Button type="button" size="icon" variant="ghost" onClick={() => onRemove(i)}>🗑️</Button>}
        </li>
      ))}
    </ul>
  </div>
);

const DisziplinenList = ({ disziplinen, onEdit, onRemove }) => (
  <div>
    <label className="block font-medium mb-1">Disziplin</label>
    <ul>
      {disziplinen.map((d, i) => (
        <li key={i} className="flex items-center gap-2 mb-1">
          <span>{d.name}</span>
          {onEdit && <Button type="button" size="icon" variant="ghost" onClick={() => onEdit(i)}>✏️</Button>}
          {onRemove && <Button type="button" size="icon" variant="ghost" onClick={() => onRemove(i)}>🗑️</Button>}
        </li>
      ))}
    </ul>
  </div>
);

// Hilfsfunktion zum Mapping der Gruppenstruktur auf TreeView
function mapGroupsToTree(groups) {
  return groups.map(g => ({
    id: g.id,
    name: g.title,
    icon: g.subParticipationGroups && g.subParticipationGroups.length > 0 ? Folder : File,
    children: g.subParticipationGroups && g.subParticipationGroups.length > 0 ? mapGroupsToTree(g.subParticipationGroups) : undefined,
    actions: (
      <Button asChild size="icon" variant="ghost">
        <Link to={`/participant-groups/${g.id}/edit`}><Pencil /></Link>
      </Button>
    ),
  }));
}

const CompetitionDetail = () => {
  const { id } = useParams();
  const { competitions } = useData();
  const competition = competitions.find((c) => c.id === parseInt(id));
  const [dialogOpen, setDialogOpen] = useState(false);
  const [newGroupName, setNewGroupName] = useState('');
  // TODO: persistieren, aktuell nur Demo
  const [groups, setGroups] = useState(competition?.participantGroups || []);

  const handleAddGroup = () => {
    if (newGroupName.trim()) {
      setGroups([...groups, { id: Date.now().toString(), name: newGroupName }]);
      setNewGroupName('');
      setDialogOpen(false);
    }
  };

  if (!competition) return <div>Wettbewerb nicht gefunden</div>;

  return (
    <main className="min-h-screen w-full px-4 py-10 bg-background">
      <div className="max-w-7xl mx-auto">
        <div className="flex items-center justify-between mb-8 border-b pb-2">
          <h2 className="text-3xl font-bold">{competition.name}</h2>
          <Button asChild variant="outline" className="ml-4">
            <Link to={`/competitions/${id}/leaderboard`}>Leaderboard öffnen</Link>
          </Button>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 items-start">
          <div className="flex flex-col gap-8 col-span-2">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
              <div>
                <label className="block font-medium mb-1">Bezeichnung</label>
                <input className="w-full border border-gray-300 rounded-md bg-white px-3 py-2 text-black" value={competition.name} readOnly />
              </div>
              <div>
                <label className="block font-medium mb-1">Datum</label>
                <input className="w-full border border-gray-300 rounded-md bg-white px-3 py-2 text-black" value={competition.date || ''} readOnly />
              </div>
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
              <KlassenList klassen={competition.klassen || []} />
              <SchreiberList schreiber={competition.schreiber || []} />
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
              <DisziplinenList disziplinen={competition.disziplinen || []} />
            </div>
          </div>
          <div className="col-span-1 flex flex-col items-center">
            <label className="block font-medium mb-2">Teilnehmergruppen</label>
            <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
              <DialogTrigger asChild>
                <Button className="mb-4 w-full" variant="outline">Gruppe hinzufügen</Button>
              </DialogTrigger>
              <DialogContent>
                <DialogHeader>
                  <DialogTitle>Neue Teilnehmergruppe</DialogTitle>
                </DialogHeader>
                <Input
                  placeholder="Gruppenname"
                  value={newGroupName}
                  onChange={e => setNewGroupName(e.target.value)}
                  className="mb-4"
                />
                <DialogFooter>
                  <Button onClick={handleAddGroup} disabled={!newGroupName.trim()} className="bg-black text-white hover:bg-black/80">Speichern</Button>
                  <DialogClose asChild>
                    <Button variant="secondary">Abbrechen</Button>
                  </DialogClose>
                </DialogFooter>
              </DialogContent>
            </Dialog>
            <div className="w-full mt-4 bg-white border rounded-lg p-2 shadow-sm min-h-[80px]">
              <TreeView data={mapGroupsToTree(groups)} />
            </div>
          </div>
        </div>
        <div className="flex justify-between mt-16">
          <Button asChild variant="outline">
            <Link to="/competitions">Zurück</Link>
          </Button>
          <Button asChild variant="outline">
            <Link to={`/competitions/${id}/edit`}>Bearbeiten</Link>
          </Button>
        </div>
      </div>
    </main>
  );
};

export default CompetitionDetail;
