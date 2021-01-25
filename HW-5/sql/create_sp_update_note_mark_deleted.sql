CREATE FUNCTION set_note_as_delete (id UUID)
RETURNS TABLE(modified_at timestamp with time zone) AS $$
UPDATE public.notes
	SET is_deleted=TRUE, modified_at=CURRENT_TIMESTAMP
	WHERE notes.id=set_note_as_delete.id;
SELECT modified_at FROM notes WHERE id=set_note_as_delete.id;
$$ LANGUAGE SQL;