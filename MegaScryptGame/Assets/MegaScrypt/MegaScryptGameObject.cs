using System;
using UnityEngine;

public class MegaScryptGameObject : MegaScrypt.Object
{
    private GameObject _target;
    public GameObject Target => _target;

    public MegaScryptGameObject(GameObject target)
    {
        _target = target;
        Bind();
    }

    private void Bind()
    {
        Declare("name", () => _target.name, (object name) =>
        {
            _target.name = (string)name;
        });

        Declare("x",
        () => _target.transform.position.x,
        (object x) =>
        {
            Vector3 newPosition = new Vector3
            {
                x = Convert.ToSingle(x),
                y = _target.transform.position.y,
                z = _target.transform.position.z
            };

            _target.transform.position = newPosition;
        });

        Declare("y",
        () => _target.transform.position.y,
        (object y) =>
        {
            Vector3 newPosition = new Vector3
            {
                x = _target.transform.position.x,
                y = Convert.ToSingle(y),
                z = _target.transform.position.z
            };

            _target.transform.position = newPosition;
        });

        Declare("z",
        () => _target.transform.position.z,
        (object z) =>
        {
            Vector3 newPosition = new Vector3
            {
                x = _target.transform.position.x,
                y = _target.transform.position.y,
                z = Convert.ToSingle(z)
            };

            _target.transform.position = newPosition;
        });

        Declare("width", () => _target.transform.localScale.x);
        Declare("height", () => _target.transform.localScale.y);

        Declare("color",
        () => _target.GetComponent<MeshRenderer>().material.color,
        (object color) =>
        {
            string colorString = (string)color;
            MeshRenderer renderer = _target.GetComponent<MeshRenderer>();

            if (ColorUtility.TryParseHtmlString(colorString, out Color color1))
            {
                renderer.material.color = color1;
            }
        });
    }
}
