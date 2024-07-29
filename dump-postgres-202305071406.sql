--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-05-07 14:06:42

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3330 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 19219)
-- Name: map; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.map (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    description character varying(800),
    columns integer NOT NULL,
    rows integer NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifieddate timestamp without time zone NOT NULL
);


ALTER TABLE public.map OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 19218)
-- Name: map_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.map ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.map_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 218 (class 1259 OID 19225)
-- Name: robotcommand; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.robotcommand (
    id integer NOT NULL,
    "Name" character varying(50) NOT NULL,
    description character varying(800),
    ismovecommand boolean NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifieddate timestamp without time zone NOT NULL
);


ALTER TABLE public.robotcommand OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 19224)
-- Name: robotcommand_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.robotcommand ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.robotcommand_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 3322 (class 0 OID 19219)
-- Dependencies: 216
-- Data for Name: map; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.map (id, name, description, columns, rows, createddate, modifieddate) FROM stdin;
1	mini s	25-25 square	25	25	2023-04-26 00:00:00	2023-04-26 00:00:00
2	medium s	50-50 square	50	50	2023-04-26 00:00:00	2023-04-26 00:00:00
3	large s	100-100 square	100	100	2023-04-26 00:00:00	2023-04-26 00:00:00
4	mini r	25-50 rectangle	25	50	2023-04-26 00:00:00	2023-04-26 00:00:00
5	medium r	100-50 rectangle	100	50	2023-04-26 00:00:00	2023-04-26 00:00:00
\.


--
-- TOC entry 3324 (class 0 OID 19225)
-- Dependencies: 218
-- Data for Name: robotcommand; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.robotcommand (id, "Name", description, ismovecommand, createddate, modifieddate) FROM stdin;
1	PLACE	place robot on the map	f	2023-04-26 00:00:00	2023-04-26 00:00:00
2	LEFT	turn left in the same coordinate	t	2023-04-26 00:00:00	2023-04-26 00:00:00
3	RIGHT	turn right in the same coordinate	t	2023-04-26 00:00:00	2023-04-26 00:00:00
4	MOVE	move one coordinate in the same direction	t	2023-04-26 00:00:00	2023-04-26 00:00:00
5	REPORT	returns the coordinates and direction	f	2023-04-26 00:00:00	2023-04-26 00:00:00
\.


--
-- TOC entry 3331 (class 0 OID 0)
-- Dependencies: 215
-- Name: map_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.map_id_seq', 5, true);


--
-- TOC entry 3332 (class 0 OID 0)
-- Dependencies: 217
-- Name: robotcommand_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.robotcommand_id_seq', 5, true);


-- Completed on 2023-05-07 14:06:44

--
-- PostgreSQL database dump complete
--

