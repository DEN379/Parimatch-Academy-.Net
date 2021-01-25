-- Table: public.notes

-- DROP TABLE public.notes;

CREATE TABLE public.notes
(
    id uuid NOT NULL,
    header character varying(128) COLLATE pg_catalog."default" NOT NULL,
    body character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    is_deleted boolean NOT NULL,
    user_id integer NOT NULL,
    modified_at timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT pk_user_id PRIMARY KEY (id),
    CONSTRAINT fk_user_id FOREIGN KEY (user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.notes
    OWNER to postgres;
-- Index: index_modifing

-- DROP INDEX public.index_modifing;

CREATE INDEX index_modifing
    ON public.notes USING btree
    (modified_at ASC NULLS LAST)
    TABLESPACE pg_default;